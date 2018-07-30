from connect import Connection
import pandas as pd
import sqlite3
from sklearn import svm
import numpy as np
import sklearn
from sklearn.cross_validation import train_test_split


class queriesClass:
    """class for queries about limits etc."""
    db=Connection()
    merged1=None
    user_id=None
    userPreferences=None 
    count=0
    list_length=25
    dfList=None
    toPrint=None
    alreadyTry=False
    userNum=0

    def __init__(self):
        self.db.connect()

#here we get the user \new user id from the sign up system
    def return_suggested_products(self, userID):
  
        #for now we are not counting the avarage list length, because we still
        #dont have enough receipts and the function returns small amount (10), so after checking the avarage 
        #receipts size for family, and suggested list size for existing costumer, we decide to take first 25 products
        #and suggedst it to new user
        #when our data will grow, we can use this function
        ##############self.countAvarageListSize()

        query="SELECT [limit_id] FROM  [db_shopagent].[dbo].[user_ref_limits] WHERE [user_id]="+userID
        self.userPreferences=pd.DataFrame(pd.read_sql(query,self.db.connection))#get user limits
        self.user_id=userID #save userID for later if we will need it

        #if the user do not exist\have no limits
        if(self.userPreferences.empty):
            self.noLimits()

        #if the user have only one limit
        elif(len(self.userPreferences.index)==1):
            self.count=0
            #self.checkIfPartialOrNot(self.userPreferences.iloc[0]['limit_id'])
            self.returnDfOfOneLimitQuery(self.userPreferences)
        
        #if the user have few limits
        else:
            #print(userPreferences.iloc[0]['limit_id'])
            self.returnFewLimitsQuery(self.userPreferences)

        #there is no option to get empty list, because even if a mistake accured and 
        #there is no user with the given id, we still want to return suggestion list

        #after all the calculations we return the suggestion
        self.printToCsv(self.dfList)
        return (self.dfList)
    
    

    #if the user have no limits, we take from the db array of all existing limits, because
    #we can reccomend everything for this user 
    def noLimits(self):
        query="SELECT [limit_id] FROM  [db_shopagent].[dbo].[user_ref_limits]"
        userPreferences=pd.DataFrame(pd.read_sql(query,self.db.connection))#get all limits
        self.alreadyTry=False
        self.returnFewLimitsQuery(userPreferences)

    #a function to check if the preferences of the user is partial (only part of the family have this limit)
    def checkIfPartialOrNot(self,limitId):
        #variable user_id saves the info of the user
        queryForPart="SELECT [Partial] FROM [db_shopagent].[dbo].[user_ref_limits] WHERE [limit_id]="+str(limitId)+" And [user_id]="+str(self.user_id)
        val=pd.DataFrame(pd.read_sql(queryForPart,self.db.connection))#get user limits

        #if the match not partial we can continue to finding the list
        if(val.iloc[0]['Partial']==0):
            self.returnDfOfOneLimitQuery(limitId)

        #what should we do if the user have only one limit and it's partial?
        else:
            self.learningForPatial()




    #function to count avarage list size, for now the database not big enough, so we decided to give a reccomendation of 25
    #products
    def countAvarageListSize(self):
        countReceipt=pd.DataFrame(pd.read_sql("SELECT COUNT (*) FROM [db_shopagent].[dbo].[receipt]",self.db.connection))
        countProducts=pd.DataFrame(pd.read_sql("SELECT COUNT (*) FROM [db_shopagent].[dbo].[receipt]",self.db.connection))
        a=int(countReceipt.iloc[0])
        b=int(countProducts.iloc[0])
        self.list_length=round(a/b)*10
      

    #we use this function if we get empty suggestion list, because then its relevant
    def learningForPatial(self,limitNum):
        #if i'm partial or full and there is no suggested list for the user, i can learn from any one- and from those who are single too. 
        #after that i'm doing merge, and sending it to calculate list, to find the best options

        #when the user have few limits we get an int variable, and when there is 
        #only one limit we get a data frame, so we have to check what do we get
        #to use the right variable in our query
        if(self.count==0):
                   a=limitNum.iloc[0]['limit_id']
                   query="SELECT [user_id] FROM [db_shopagent].[dbo].[user_ref_limits] WHERE [limit_id]="+str(a)#+" AND [Partial]=0"
                   A=pd.DataFrame(pd.read_sql(query,self.db.connection))#users wity "full" limit dataframe

        else:

            A = pd.DataFrame(columns=['user_id'])
            if(limitNum.size>1):
                index=0            
                for index, row in limitNum.iterrows():
                    query="SELECT [user_id] FROM [db_shopagent].[dbo].[user_ref_limits] WHERE [limit_id]="+str(row['limit_id'])#+" AND [Partial]=0"
                    few_values[index]=pd.DataFrame(pd.read_sql(query,self.db.connection))#users wity "full" limit dataframe
            
                index=0
                while (index < len(dataframe_collection)):
                    A = pd.concat([A, dataframe_collection[index]])
                    index=index+1                                        
        
        
        self.user_id=A.size
        query="SELECT [user_id] FROM [db_shopagent].[dbo].[user_ref_limits] WHERE [limit_id]="+str(a)+" AND [Partial]=1"
        query_shoppingList="SELECT [user_id],[receipt_id] FROM [db_shopagent].[dbo].[receipt_ref_user]"
        query_items="SELECT [receipt_id],[product_id],[quantity] FROM [db_shopagent].[dbo].[receipet_ref_product]"

        queryForFew="SELECT [limit_id] FROM  [db_shopagent].[dbo].[user_ref_limits]"
        userPreferences=pd.DataFrame(pd.read_sql(queryForFew,self.db.connection))#get all limits

        #data frame of all users with choosen 
        A=pd.DataFrame(pd.read_sql(query,self.db.connection))#users by limits
        B=pd.DataFrame(pd.read_sql(query_shoppingList,self.db.connection))#receipt by users
        C=pd.DataFrame(pd.read_sql(query_items,self.db.connection))#products by receipy
  
        A.append(userPreferences)
        if(A.empty or B.empty):
             self.merged1=None
             self.noLimits()

        else:
            #only users and receipt
            merged=pd.merge(A,B, on='user_id')

            #user receipt and products
            self.merged1=pd.merge(merged,C, on='receipt_id')
            #print(merged,'\n')
            #print(self.merged1,'\n')


            #i dont need the user or the receipt id anymore
            self.merged1.__delitem__('user_id')
            self.merged1.__delitem__('receipt_id')
            #print(self.merged1)


            #how many users - different- we have
            n_users=merged.user_id.unique().shape[0]
            #how many differen products we have
            n_product=self.merged1.product_id.unique().shape[0]
            #print(n_users,n_product)

            #i collecting the items to groups- by counting the numbers they were bought
            self.merged1.groupby('product_id')
            #print(self.merged1)

            self.alreadyTry=True
            self.calculateList(limitNum)

            #if after all the manipulation we still don't have suggested list for the costumer we will give him
            #basic suggested list for user with no limits
            if(self.dfList==None):
                self.noLimits()


    #there is similarity to leartningForPartial function, but here we choose only users with "full" limits
    #where the limits is not partial 
    def returnDfOfOneLimitQuery(self, limitNum):

        #when the user have few limits we get an int variable, and when there is 
        #only one limit we get a data frame, so we have to check what do we get
        #to use the right variable in our query
        if(self.count==0):
           a=limitNum.iloc[0]['limit_id']

        else:
           a=limitNum

        query="SELECT [user_id] FROM [db_shopagent].[dbo].[user_ref_limits] WHERE [limit_id]="+str(a)+" AND [Partial]=0"
        query_shoppingList="SELECT [user_id],[receipt_id] FROM [db_shopagent].[dbo].[receipt_ref_user]"
        query_items="SELECT [receipt_id],[product_id],[quantity] FROM [db_shopagent].[dbo].[receipet_ref_product]"

        queryOnlyOne="""select [user_id] from [db_shopagent].[dbo].[user_ref_limits] where [user_id] in 
        (select user_id from [db_shopagent].[dbo].[user_ref_limits] 
                    group by [user_id],[limit_id]
                   having count(*) =1 and [limit_id]="""+str(limitNum)+") AND [Partial]=0"

        print("i'm here")

        A=pd.DataFrame(pd.read_sql(query,self.db.connection))#users wity "full" limit dataframe
        B=pd.DataFrame(pd.read_sql(query_shoppingList,self.db.connection))#receipt by users
        C=pd.DataFrame(pd.read_sql(query_items,self.db.connection))#products by receipy
  
        #we can get empty data frames of users with the limits that we need. 
        #but in the function of few limits we have to check all the limits, and it 
        #not important for us in this function to get non empty array, because we will take care of it 
        #at the function
        
        self.user_id=A.size

        #only users and receipt
        merged=pd.merge(A,B, on='user_id')

        #user receipt and products
        self.merged1=pd.merge(merged,C, on='receipt_id')
        #print(merged,'\n')
        #print(self.merged1,'\n')


        #i dont need the user or the receipt id anymore
        self.merged1.__delitem__('user_id')
        self.merged1.__delitem__('receipt_id')
        #print(self.merged1)


        #how many users - different- we have
        n_users=merged.user_id.unique().shape[0]
        #how many differen products we have
        n_product=self.merged1.product_id.unique().shape[0]
        #print(n_users,n_product)

        #i collecting the items to groups- by counting the numbers they were bought
        #self.merged1.groupby('product_id')['quantity'].sum()
        #print(self.merged1)
        

        if(self.count==0):
            self.calculateList(limitNum)



    def calculateList(self,limitsArray):

        #self.merged1 = self.merged1.groupby(['product_id']).agg({'quantity': 'count'}).reset_index()
        #print(self.merged1)
        #grouped_sum =  products_grouped['quantity'].median()
        #products_grouped['percentage']  = products_grouped['quantity'].div(grouped_sum)*100
        #products_grouped.sort_values(['quantity', 'product_id'], ascending = [0,1])
        #ab=self.merged1.loc[self.merged1['product_id'] == 573] 
        #n=ab['quantity'].median()
        #dfMed.loc[0] = [str(ab.iloc[0]['product_id']),n]
        #print (dfMed)
        #dfMed.iloc[0]=sign

        #i prefered not delete this part because it worked well enough and if there would be an errors 
        #maybe i will use part of it
        ###################################################################################################

        mm = pd.DataFrame(columns=['product_id','quantity'])
        
        #we don't want that one user will affect all the list (for example 150 milk bottels was bought by one user)
        #so we calculate the median
        mm=self.merged1.groupby('product_id')['quantity'].median().reset_index()

        #if the suggested list size (25 for now) smaller then the suggested shopping list
        #we will return less products
        if(self.list_length>mm.size):
            self.list_length=mm.size

        #we take the products with largest median 
        #we take the products with largest median 
        mm=mm.nlargest(self.list_length, 'quantity')

        self.dfList = mm['product_id'].tolist()        
        print('Ans:'+str(self.dfList))        
        if(self.dfList==None ):
            self.learningForPatial(limitsArray)

            
#        self.printToCsv(self.dfList)
        #print print
    #when we get few limits for one user 
    def returnFewLimitsQuery(self,limitsArray):
        df = pd.DataFrame(columns=['product_id','quantity'])
        df1 = pd.DataFrame(columns=['product_id','quantity'])
        df2 = pd.DataFrame(columns=['product_id','quantity'])
        df3 = pd.DataFrame(columns=['product_id','quantity'])

        isinA={}
        self.count=1 #variable to use in another functions, because we use there a function of
        dataframe_collection={}

        #"oneLimitQuery, we need to return to our function and this variable help us to decide 
        if(self.alreadyTry==False):

            for index, row in limitsArray.iterrows():
                self.returnDfOfOneLimitQuery(row['limit_id'])
                #we append all the dataframes that we get after calculation of every limit by itself
                #and then we send it to "calculate list   
                dataframe_collection[index]=self.merged1
                self.merged1=None
                #if(df.empty):
                 #   sign=[df,self.merged1]
                  #  df=pd.concat(sign)
                #else:
                 #   df.append(self.merged1)

            index=0

          #  while (index < len(dataframe_collection)):
           #     df.append(dataframe_collection[index].groupby(["product_id"])["quantity"].sum())
            #    index=index+1

           

            index=0
            while (index< len(dataframe_collection)):
                sign=[df,dataframe_collection[index]]
                df=pd.concat(sign)
                #df = df.groupby('product_id')[ 'quantity'].sum()
                df1=df
                df1['Total'] =  df.groupby('product_id')['quantity'].sum()
                df1=df1.dropna(axis=0, how='all') #remve all row where all value is 'NaN' exists
                if(df2.empty):
                    sin=[df1,df2]
                    df2=pd.concat(sin)
                else:
                    df2.append(df1)
                index=index+1
            
            df2['Total']=df2['Total'] =  df.groupby('product_id')['quantity'].sum()
            df2=df = df2[pd.notnull(df2['Total'])] #remve all row where all value is 'NaN' exists
            df2['quantity']=df2['Total']
            df2 = df2.drop('Total', 1)

            #print(df2.sort_values('product_id', ascending=False))
            df1=df2.groupby('product_id').count()
            print(df1.sort_values('quantity',ascending=True))
            self.merged1=df2

            self.alreadyTry=True
            self.calculateList(limitsArray)

        else:
            if(self.count==0):
                self.learningForPatial(limitsArray)

            else:
                self.dfList=None
                return




    #at the futer if we will use a server instade of calculating new list every time
    #we can save template of suggested list for every limit
    #and recalculate it only at the end of the day for example

    def printToCsv(self, dfta):
          df = pd.DataFrame(columns=['product_id','product_name'])
          for row in dfta:
            A=pd.DataFrame(pd.read_sql("SELECT [product_id],[product_name] FROM [db_shopagent].[dbo].[products] WHERE [product_id]="+str(row),self.db.connection))#users by limits
            sign=[df,A]
            df=pd.concat(sign)
          df.to_csv("cvName",encoding='utf-8')
         

