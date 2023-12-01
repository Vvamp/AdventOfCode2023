input_filename = "input.txt"
input_data = ""

with open(input_filename, "r") as input_file:
    input_data = input_file.readlines()


number_declarations = {
    # "zero": 0,
    "one": 1,
    "two": 2,
    "three": 3,
    "four": 4,
    "five": 5,
    "six": 6,
    "seven": 7,
    "eight": 8,
    "nine": 9,
}


class IdentifiedDigit:
    def __init__(self, digit_string, start_index, line):
        self.digit = number_declarations[digit_string]
        self.digit_string = digit_string
        self.start_index = start_index
        self.full_string = line
        self.end_index = start_index + len(self.digit_string)


def formatNumberTextsToDigits(puzzle_input: str) -> str:
    for line_index in range(0, len(puzzle_input)):
        line = puzzle_input[line_index].replace("\n", "")
        for char_index in range(0, len(line)):
            for key in number_declarations.keys():
                if line[char_index:].startswith(key):
                    line_list = list(line)
                    line_list[char_index + 1] = str(
                        number_declarations[key]
                    )  # I am replacing the SECOND character in each number word, as these aren't counted in the overlapping number strings. VERY UGLY. I know. But I wanted to keep the getCalibrationNumberSum unchanged
                    line = "".join(line_list)
        puzzle_input[line_index] = line
    return puzzle_input


def getCalibrationNumberSum(puzzle_input: str):
    nums = []
    for calibration_line in puzzle_input:
        digits = [c for c in calibration_line if c >= "0" and c <= "9"]
        num = int(str(digits[0]) + str(digits[-1]))
        nums.append(num)

    return sum(nums)


print(f"Total sum: {getCalibrationNumberSum(formatNumberTextsToDigits(input_data))}")
