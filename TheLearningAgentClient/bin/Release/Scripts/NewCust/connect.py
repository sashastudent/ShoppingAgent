import pandas as pd
import sklearn
import pyodbc
import csv

class Connection:
    connection=None

    def __init__(self):
        server ='.\SQLEXPRESS'
        db ='db_shopagent'
        self.conn_str='DRIVER={SQL Server};SERVER=' + server + ';DATABASE' + db + ';Trusted_Connection=yes'
       
    def connect(self):
        self.connection=pyodbc.connect(self.conn_str)
        if self.connection:
            print("conn")
        else:
            print("problem")
        
    def makeQuery(self, strQuery):
      if self.connection:
          print("connectd")
      try:
          return (self.connection.execute(strQuery))
      except Exception as e:
          print("exeption accured ",e)

    def endConnection(self):
        self.connection.close()


        