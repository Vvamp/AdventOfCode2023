from typing import List


def returnDataByLines(filename: str) -> List[str]:
    with open(filename, "r") as input_file:
        return input_file.readlines()


def returnData(filename: str) -> str:
    with open(filename, "r") as input_file:
        return input_file.read()
