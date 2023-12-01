input_filename = "input.txt"
input_data = ""

with open(input_filename, "r") as input_file:
    input_data = input_file.readlines()


def getCalibrationNumberSum(puzzle_input: str):
    nums = []
    for calibration_line in input_data:
        digits = [c for c in calibration_line if c >= "0" and c <= "9"]
        # print(f"{digits[0]} + {digits[-1]}")
        num = int(str(digits[0]) + str(digits[-1]))
        nums.append(num)

    return sum(nums)


print(f"Total sum: {getCalibrationNumberSum(input_data)}")
