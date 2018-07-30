import pandas as pd
import numpy as np
import pyodbc
from pandas import Series, DataFrame
import matrix_factorization_utilities
from sklearn.model_selection import train_test_split
import pickle

class user_agent_class(object):
    """This class Responsible for learning the purchasing habits of the user 
    Major Data frames: 
    user_product_purch_df - contain ordered usere's,
                            ordered theirs purchases products 
                            and quantity of product purchases. 
                            not include information 
                            about products that never purchased
                            and user that never do purchas.
    products_df -           containing informaton on products.  
    ordered_products-       contain ordered poducts that purchased by user at least once.
    ordered_users -         contain ordered user that purchased at least once.
    df -                    base clean data, contain: product_num, user_num, quantity
    """

    def setDataFrames(self):
        """
        Create and sets data farems use db querys
        """
        conn = self.connectionDB();

        #query db for get table products
        qry = 'SELECT [product_id],[product_name],[weighable] FROM [db_shopagent].[dbo].[products]'
        products = self.read_query(qry, conn)
        #self.products_delils = products
        
        #query db for get table receipet_ref_product
        qry = 'select * from [db_shopagent].[dbo].[receipet_ref_product]'
        receipet_ref_product = self.read_query(qry, conn)  

        #create product df only with product from reciepts 
        self.ordered_products = receipet_ref_product.sort_values(by=['product_id'])
        self.ordered_products = self.ordered_products.drop(['receipt_id','quantity'], axis=1)
        self.ordered_products = self.ordered_products[~ self.ordered_products.product_id.duplicated(keep='first')].reset_index(drop=True)
        self.ordered_products.index = np.arange(1,len(self.ordered_products)+1)
        self.ordered_products = self.ordered_products.reset_index(drop=False)
        self.ordered_products.columns = ['product_num','product_id']
        self.ordered_products.to_csv("ordered_productst.csv", encoding='utf-8-sig')

        products = pd.merge(self.ordered_products, products)
  
        receipet_ref_product = pd.merge(receipet_ref_product, self.ordered_products)

        #query db for get table receipt_ref_user
        qry = 'select * from [db_shopagent].[dbo].[receipt_ref_user]'
        receipt_ref_user = self.read_query(qry, conn)
 
        #create user df only with product from reciepts 
        self.ordered_users = receipt_ref_user.drop(['receipt_id'], axis=1)
        self.ordered_users = self.ordered_users[~ self.ordered_users.user_id.duplicated(keep='first')].reset_index(drop=True)
        self.ordered_users.index = np.arange(1,len(self.ordered_users)+1)
        self.ordered_users = self.ordered_users.reset_index(drop=False)
        self.ordered_users.columns = ['user_num','user_id']
        self.ordered_users.to_csv("ordered_users.csv", encoding='utf-8-sig')

        receipt_ref_user = pd.merge(receipt_ref_user, self.ordered_users)
  
        self.close_connecionDB(conn)

        #Inner join 
        df = pd.merge(receipet_ref_product, receipt_ref_user)

        #clean df 
        self.df = df[['product_num', 'quantity', 'user_num']]#only nededd columns
        self.user_product_purch_df = df.groupby(['user_num', 'product_num']).sum().reset_index() # Group together

        #create product df with ordered num products and name
        products_df = products[['product_num','product_name','weighable']]
        self.products_df = products_df


    def stratLearn(self):
        """
        Learn user use other users by matrix fuctorization mathod.
        first create pivot table matrix of users, products and purchases.
        create two matrix by factorization matrix :
        U-users features 
        P-products features
        predicted purchases list get from multiplied U and P matrix 
        """
        self.setDataFrames()

        #normalize quntity
        self.user_product_purch_df_normalized = self.normalize_quantity()
        
        purchases = pd.pivot_table(self.user_product_purch_df_normalized, index='user_num', columns='product_num', aggfunc=np.max)
        U, P = matrix_factorization_utilities.low_rank_matrix_factorization(purchases.as_matrix(), num_features=15, regularization_amount=3.6)    
        predicted_purchases = np.matmul(U,P)
        P = np.transpose(P)
        pickle.dump(U, open("user_features.dat", "wb"))
        pickle.dump(P, open("product_features.dat", "wb"))
        pickle.dump(predicted_purchases, open("predicted_purchases.dat", "wb"))

        rmse = matrix_factorization_utilities.RMSE(purchases.as_matrix(),   predicted_purchases)
        #print(rmse)

    def getActual_User_Purchased_List(self, userId):
        """
        Internal function that get actual user purchases 
        :param userId: user id for recognize user purchases
        :return actual_user_purchases_merge: returned df of actual user purchases 
        """
        user_num_to_search = userId
        #user's actual product purchases, merge for do not getting producs which were never bought by eny user
        actual_user_purchases = self.user_product_purch_df_normalized[self.user_product_purch_df_normalized['user_num'] == user_num_to_search]      
        actual_user_purchases_merge = pd.merge(self.products_df, actual_user_purchases, on='product_num')
        return actual_user_purchases_merge


    def create_IDs_products_list_from_product_nums(self, recommended_df):
        """
        Create list of id's products from df that include prodcts nums. 
        :param recommended_df: data frame that includs products numbers that needed convert to list with products ides.
        :return products_ids_list: returned list ides of products 
        """
        list = pd.merge(self.ordered_products, recommended_df, on='product_num')
        products_ids = list['product_id']
        products_ids_list = products_ids.values.T.tolist()
        return products_ids_list


    def getRecommendsList(self,userId):   
        """
        Returned recommended user list that combined with purchases habits list and not which were not bought by this user
        but that will he might want to purchas.
        use U, P, predicted_purchases dat fiels thet created in stratLearn function.

        :param userId: get userid to for recognize and create recommended purch list for this user.
        :return products_ids_list: list of recommended products ides, 
        combined list with habits purchases products ides and might want to purchas products ides
        """

        print("1")
        user_id_to_search = userId
        print("2")
        idx = (self.ordered_users['user_id'] == user_id_to_search) 
        print("3")
        user_num_to_search = self.ordered_users.loc[idx,'user_num'].iat[0]
        print("4")

        U = pickle.load(open("user_features.dat", "rb"))
        print("5")
        P = pickle.load(open("product_features.dat", "rb"))
        print("6")
        predicted_purchases = pickle.load(open("predicted_purchases.dat", "rb"))
        print("7")
        #get actual user purchases list
        actual_user_purchases_merge = self.getActual_User_Purchased_List(user_num_to_search)
        print("8")
        #user_purchases also his ratings on products
        user_purchases = predicted_purchases[user_num_to_search-1]
        print("9")
        product_df_and_user_prediction = self.products_df
        print("10")
        #add purchases rating user column
        product_df_and_user_prediction['purchases'] = user_purchases
        print("11")

        already_purchases = actual_user_purchases_merge['product_num']
        print("12")
        recommended_habits_purchases_df = self.recommendedHabitsList(already_purchases)
        print("13")
        recommended_new_purchases_df = self.recommendedNewList(already_purchases)
        print("14")
        products_ids_habits_list = self.create_IDs_products_list_from_product_nums(recommended_habits_purchases_df)
        print("15")
        products_ids_new_list = self.create_IDs_products_list_from_product_nums(recommended_new_purchases_df)
        print("16")
        products_ids_list =  products_ids_habits_list + products_ids_new_list
        print("17")
        return products_ids_list


    def recommendedHabitsList(self, already_purchases):
        """
        create predicted habits products list that learn on user purchases 
        habits sorted by quntity purchases from high to low
        """
        recommended_habits_purchases_df = self.products_df[self.products_df.index.isin(already_purchases) == True]
        recommended_habits_purchases_df = recommended_habits_purchases_df.sort_values(by = ['purchases'], ascending = False)#sorted by quntity purchases  
        recommended_habits_purchases_df = recommended_habits_purchases_df[['product_name','purchases','product_num']].head(20)
        #self.printToConsol(recommended_habits_purchases_df, "habits")
        return recommended_habits_purchases_df

    def recommendedNewList(self, already_purchases):
        """
        create recommended not purchases yet products sorted by quntity purchases
        from high to low
        """
        recommended_new_purchases_df = self.products_df[self.products_df.index.isin(already_purchases) == False] 
        recommended_new_purchases_df = recommended_new_purchases_df.sort_values(by = ['purchases'], ascending = False)#sorted by quntity purchases
        recommended_new_purchases_df = recommended_new_purchases_df[['product_name','purchases','product_num']].head(20)
        #self.printToConsol(recommended_new_purchases_df, "newlist")
        return recommended_new_purchases_df

    def printToConsol(self, df, dfnama):
        print(dfnama)
        print(df)
  

    def normalize_quantity(self):
        """
        mormalize users quantity of purchases weighable products
        param user_product_purch_df - include users theirs purchases products and quantity of purchases.
        param: user_product_purch_df_median- finding meadian of purchases weighable products
        param: user_product_purch_df_count- finding purchase count of weighable products of each user
        mearg data frames and keep only the weighable products
        calculate quantity of each product by each user again- The number of times the product was bought by the user multiplied by the median purchase
        of the product
        param normalized_df - returned normalized data frame.
        """
        user_product_purch_df_copy= self.user_product_purch_df
        user_product_purch_df_and_produts_df = pd.merge(self.products_df, user_product_purch_df_copy, on='product_num')

        user_product_purch_df_count = self.df.groupby(['product_num','user_num']).count()["quantity"].reset_index(name="count") # Group together
        user_product_purch_df_median = self.df.groupby(['product_num']).median()["quantity"].reset_index(name="median") # Group together
    
        normalized_df_marge = pd.merge(user_product_purch_df_and_produts_df, user_product_purch_df_count)
        normalized_df_marge = pd.merge(normalized_df_marge,user_product_purch_df_median)
        normalized_df_marge.quantity.loc[(normalized_df_marge.weighable == True)] = normalized_df_marge["count"]*normalized_df_marge["median"]
        normalized_df = normalized_df_marge[['product_num','user_num','quantity']]
        return normalized_df

  
       
    def connectionDB(self):        
        """
        Connecting to DB 
        """
        # Parameters
        server ='.\SQLEXPRESS'
        db ='db_shopagent'
        #Create the connection
        try:
            connection = pyodbc.connect('DRIVER={SQL Server};SERVER=' + server + ';DATABASE' + db + ';Trusted_Connection=yes')
            return connection
        except Exception as e:
               print("Couldn't connect: ",e)

    def close_connecionDB(self,conn):
        """
        Close cinnection
        """
        conn.close()

    def read_query(self, qry, connention):
        """
        Do and read query to db
        """
        try:
           return pd.read_sql(qry, connention)
        except Exception as e:
           print("Query do not commit: ",e)


   

        
   

   

   







   





      