#ifndef C_AOC_2024_DISTANCE_H
#define C_AOC_2024_DISTANCE_H

typedef struct {
    int *list1;
    int *list2;
    size_t size1;
    size_t size2;
} Tuple;

typedef struct {
    int key;
    int count;
} Group;

Tuple LoadSignificantLocations(const char *filename);
void FreeSignificantLocations(Tuple *locations);
void Part1(int *list1, size_t size1, int *list2, size_t size2);
void Part2(int *list1, size_t size1, int *list2, size_t size2);

#endif //C_AOC_2024_DISTANCE_H