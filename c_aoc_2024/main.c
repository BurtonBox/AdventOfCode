#include <stdio.h>
#include "src/day1/Distance.h"

int main(void) {

    Tuple locations = LoadSignificantLocations("E:\\repositories\\burtonbox\\AdventOfCode\\c_aoc_2024\\src\\day1\\part1.data.txt");

    Part1(locations.list1, locations.size1, locations.list2, locations.size2);
    Part2(locations.list1, locations.size1, locations.list2, locations.size2);

    FreeSignificantLocations(&locations);

    printf("done");

    return 0;
}
