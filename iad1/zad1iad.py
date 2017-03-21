import random
import json
import sys
from copy import deepcopy

if(len(sys.argv) == 1):
    sys.exit("Give me file! exit")
    
with open(sys.argv[1]) as data_file:    
    data = json.load(data_file)

neta = data["neta"]
z = data["z"]
x1 = data["x1"]
x2 = data["x2"]
w1 = data["w1"]
w2 = data["w2"]
"""ILE WARTOSCI"""
N = data["N"]
"""ILOŚĆ TRENINGOW"""
n = data["n"] 
K = data["K"]

random.seed
w = [random.uniform(w1,w2) for x in range(N)]
x = [[round(random.uniform(x1,x2),data["placesAfterDot"]) for x in range(N)] for y in range(n)]
originalW = deepcopy(w)

y = [0 for i in range(n)]
zyDist = [0 for i in range(n)]

for k in range(K):
    for j in range(n): 
        y[j] = 0
        for i in range(N):
            y[j] += x[j][i]*w[i] 
        for i in range(N):
            w[i] = w[i] + neta*(z-y[j])*x[j][i]
        zyDist[j] = abs(z-y[j])
	
for i in range(n):
    print ("Trening {}:\nX: {}\nWynik oczekiwany (z): {}\nWynik neuronowy (y): {}\nabs(z-y): {}\n".format(i+1,x[i],z,y[i],zyDist[i]))
print ("Wagi przed: {}\nWagi po: {}\n".format(originalW,w))