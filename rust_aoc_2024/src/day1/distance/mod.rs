use std::fs::File;
use std::io::{self, BufRead};
use std::collections::HashMap;
use std::path::Path;

#[derive(Debug)]
pub enum LoadError {
    IoError(String),
    ParseError(String),
    InvalidFormat(String),
}

pub fn part1(list1: &mut Vec<i32>, list2: &mut Vec<i32>) -> Result<i32, String> {
    if list1.len() != list2.len() {
        return Err("Input lists must have the same length.".to_string());
    }

    list1.sort_unstable();
    list2.sort_unstable();

    let dist: i32 = list1
        .iter()
        .zip(list2.iter())
        .map(|(a, b)| (a - b).abs())
        .sum();

    Ok(dist)
}

pub fn part2(list1: &Vec<i32>, list2: &Vec<i32>) -> Result<i32, String> {
    if list1.len() != list2.len() {
        return Err("Input lists must have the same length.".to_string());
    }

    let mut group: HashMap<i32, i32> = HashMap::new();
    for &num in list2 {
        *group.entry(num).or_insert(0) += 1;
    }

    let dist: i32 = list1
        .iter()
        .filter_map(|&item| group.get(&item).map(|&count| item * count))
        .sum();

    Ok(dist)
}

pub fn load_significant_locations(filename: &str) -> Result<(Vec<i32>, Vec<i32>), LoadError> {
    let path = Path::new(filename);
    let file = File::open(&path).map_err(|e| LoadError::IoError(e.to_string()))?;
    let reader = io::BufReader::new(file);

    let mut list1 = Vec::new();
    let mut list2 = Vec::new();

    for (index, line) in reader.lines().enumerate() {
        let line = line.map_err(|e| LoadError::IoError(e.to_string()))?;
        let mut parts = line.split_whitespace();

        let val1 = parts
            .next()
            .ok_or_else(|| LoadError::InvalidFormat(format!("Line {}: Missing first value", index + 1)))?
            .parse::<i32>()
            .map_err(|e| LoadError::ParseError(format!("Line {}: {}", index + 1, e.to_string())))?;

        let val2 = parts
            .next()
            .ok_or_else(|| LoadError::InvalidFormat(format!("Line {}: Missing second value", index + 1)))?
            .parse::<i32>()
            .map_err(|e| LoadError::ParseError(format!("Line {}: {}", index + 1, e.to_string())))?;

        if parts.next().is_some() {
            return Err(LoadError::InvalidFormat(format!(
                "Line {}: Too many values",
                index + 1
            )));
        }

        list1.push(val1);
        list2.push(val2);
    }

    Ok((list1, list2))
}