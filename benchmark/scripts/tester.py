import matplotlib.pyplot as plt

fix, ax = plt.subplots()#(2, 2)

databaseNames = ['postgresql', 'mysql']
barColors = ['orange', 'purple']

y = [3, 1]

ax.name = "Insert"
ax.bar(databaseNames, y, color=barColors)

plt.show()
