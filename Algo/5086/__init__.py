outputs = []

done = False

while not done:
    a, b = map(int, input().split())

    if a == 0 and b == 0:
        done = True
    elif a % b == 0:
        outputs.append("multiple")
    elif b % a == 0:
        outputs.append("factor")
    else:
        outputs.append("neither")

for output in outputs:
    print(output)