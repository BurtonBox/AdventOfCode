#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "Distance.h"

int compare(const void *a, const void *b) {
    return (*(int *)a - *(int *)b);
}

Group *groupNumbers(int *list, size_t size, size_t *groupCount) {

    qsort(list, size, sizeof(int), compare);
    Group *groups = malloc(size * sizeof(Group));
    if (!groups) {
        fprintf(stderr, "Memory allocation failed.\n");
        exit(EXIT_FAILURE);
    }

    size_t count = 0;
    for (size_t i = 0; i < size; i++) {
        if (count == 0 || groups[count - 1].key != list[i]) {
            groups[count].key = list[i];
            groups[count].count = 1;
            count++;
        } else {
            groups[count - 1].count++;
        }
    }

    *groupCount = count;
    return groups;
}

void Part1(int *list1, size_t size1, int *list2, size_t size2) {
    if (list1 == NULL || list2 == NULL || size1 != size2) {
        fprintf(stderr, "Input lists must have the same length.\n");
        exit(EXIT_FAILURE);
    }

    qsort(list1, size1, sizeof(int), compare);
    qsort(list2, size2, sizeof(int), compare);

    int dist = 0;
    for (size_t i = 0; i < size1; i++) {
        dist += abs(list1[i] - list2[i]);
    }

    printf("Part1 Distance: %d\n", dist);
}

void Part2(int *list1, size_t size1, int *list2, size_t size2) {
    if (list1 == NULL || list2 == NULL || size1 != size2) {
        fprintf(stderr, "Input lists must have the same length.\n");
        exit(EXIT_FAILURE);
    }

    size_t groupCount;
    Group *groups = groupNumbers(list2, size2, &groupCount);

    int dist = 0;
    for (size_t i = 0; i < size1; i++) {
        for (size_t j = 0; j < groupCount; j++) {
            if (list1[i] == groups[j].key) {
                dist += list1[i] * groups[j].count;
                break;
            }
        }
    }

    free(groups);

    printf("Part2 Distance: %d\n", dist);
}

Tuple LoadSignificantLocations(const char *filename) {
    Tuple result = {NULL, NULL};
    FILE *file = fopen(filename, "r");

    if( !file) {
        fprintf(stderr, "Failed to open file: %s\n", filename);
        exit(EXIT_FAILURE);
    }

    size_t capacity1 = 10;
    size_t capacity2 = 10;
    result.list1 = malloc(capacity1 * sizeof(int));
    result.list2 = malloc(capacity2 * sizeof(int));

    if (!result.list1 || !result.list2) {
        fprintf(stderr, "Memory allocation failed.\n");
        fclose(file);
        exit(EXIT_FAILURE);
    }

    char line[256];
    while (fgets(line, sizeof(line), file)) {
        int value1, value2;

        // Split the line into two integers
        if (sscanf(line, "%d   %d", &value1, &value2) == 2) {
            if (result.size1 >= capacity1) {
                capacity1 *= 2;
                result.list1 = realloc(result.list1, capacity1 * sizeof(int));
                if (!result.list1) {
                    fprintf(stderr, "Memory reallocation failed.\n");
                    fclose(file);
                    exit(EXIT_FAILURE);
                }
            }

            if (result.size2 >= capacity2) {
                capacity2 *= 2;
                result.list2 = realloc(result.list2, capacity2 * sizeof(int));
                if (!result.list2) {
                    fprintf(stderr, "Memory reallocation failed.\n");
                    fclose(file);
                    exit(EXIT_FAILURE);
                }
            }

            result.list1[result.size1++] = value1;
            result.list2[result.size2++] = value2;
        } else {
            fprintf(stderr, "File is corrupted.\n");
            fclose(file);
            free(result.list1);
            free(result.list2);
            exit(EXIT_FAILURE);
        }
    }
    fclose(file);
    return result;
}

void FreeSignificantLocations(Tuple *locations) {
    free(locations->list1);
    free(locations->list2);
    locations->list1 = NULL;
    locations->list2 = NULL;
    locations->size1 = 0;
    locations->size2 = 0;
}

