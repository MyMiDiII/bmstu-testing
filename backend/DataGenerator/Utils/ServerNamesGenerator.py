from math import factorial
import random

WORDS_IN_NAME = 2

lst = [
        "Minecraft", "Grief", "Creeper", "Redstone", "Enderdragon",
        "Bedrock", "Nether", "Server", "Friend", "Redstone", "Donate",
        "NoDonate", "HungerGames", "HypixelParody", "MC", "Survival",
        "Creative", "Adventure", "MC", "Apple", "Gaming", "Block"
      ]

# Получить количество сочетанй без повторений
def countCombinationsWithoutRepeats(size : int, toChoose : int):
    return int(factorial(size) / (factorial(toChoose) * factorial(size - toChoose)))


# Сгенерировать N уникальных имен серверов
def generateUniqueServerNamesSet(amount : int):

    if (len(lst) == 0):
        print("Failure: Word's set is clear\n")
        return []

    if (len(lst) < WORDS_IN_NAME):
        print("Failure: Word's set is short. Need " + str(WORDS_IN_NAME) + " words at least\n")
        return []

    maxAmount = countCombinationsWithoutRepeats(len(lst), WORDS_IN_NAME)

    if (amount >= maxAmount):
        print("Failure: Can't create more than " + str(maxAmount) + " names. Make word's set bigger\n")
        return []

    serversSet = []
    index = 0

    while (index < amount): 

        wordsIndexes = random.sample(range(0, len(lst) - 1), WORDS_IN_NAME)
        serverName = ''
        
        for wordInd in range(WORDS_IN_NAME):
            serverName += (lst[wordsIndexes[wordInd]])

        if (serverName not in serversSet):
            index += 1
            serversSet.append(serverName)

    print("Success: Server Names Generated\n")

    return serversSet
