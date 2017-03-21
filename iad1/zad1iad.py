import random
import json
import sys

if(len(sys.argv) == 1):
    sys.exit("Give me file! exit")
    
with open(sys.argv[1]) as data_file:    
    data = json.load(data_file)

neta = data["neta"]
x1 = data["x1"]
x2 = data["x2"]
w1 = data["w1"]
w2 = data["w2"]
"""ILE WARTOSCI"""
N = data["N"]
"""ILOŚĆ TRENINGOW"""
n = data["n"] 
K = data["K"]

w = [[random.uniform(w1,w2) for x in range(N)] for y in range(n)]
x = [[random.uniform(x1,x2) for x in range(N)] for y in range(n)]

z = [sum(x[i]) for i in range(n)]
y = [0 for i in range(n)]

for k in range(K):
    for j in range(n): 
        y[j] = 0
        for i in range(N):
            y[j] += x[j][i]*w[j][i] 
        for i in range(N):
            w[j][i] = w[j][i] + neta*(z[j]-y[j])*x[j][i]
for i in range(n):
    print ("Trening {}:\nX: {}\nWagi: {}\nWynik oczekiwany (z): {}\nWynik neuronowy (y): {}\n".format(i+1,x[i],w[i],z[i],y[i]))