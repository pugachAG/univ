import numpy

x = [2,4,6,8]
y = numpy.matrix([0.35, 0.573, 0.725, 0.947]).transpose()
n = 2
m = len(x)
A = numpy.matrix([[x[i] ** j for j in range(n)] for i in range(m)])
At = A.transpose()
I = numpy.linalg.inv(At * A)
a = (I * At) * y
#calc mean square deviation
f = lambda arg: sum([(arg ** i) * a[i][0] for i in range(n)])
diff = sum([(f(x[i]) - y[i]) ** 2 for i in range(m)])

print("a: ")
print(a)
print("deviation: ")
print(diff)

