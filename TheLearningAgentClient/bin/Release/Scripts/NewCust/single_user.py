import sys
from connect import Connection
from classForNewQuery import queriesClass
import pandas as pd
import sqlite3

qc = queriesClass()
qc.return_suggested_products(sys.argv[1])

