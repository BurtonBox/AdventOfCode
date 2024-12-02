use std::path::PathBuf;

mod day1 {
    pub mod distance;
}

fn main() {

    let path = PathBuf::from(env!("CARGO_MANIFEST_DIR"))
        .join("src/day1/distance/part1.data.txt");

    match day1::distance::load_significant_locations(path.to_str().expect("invalid path")) {
        Ok((mut list1, mut list2)) => {
            match day1::distance::part1(&mut list1, &mut list2) {
                Ok(dist) => println!("Part1 Distance: {}", dist),
                Err(e) => println!("Part1 Error: {}", e),
            }
            match day1::distance::part2(&list1, &list2) {
                Ok(dist) => println!("Part2 Distance: {}", dist),
                Err(e) => println!("Part2 Error: {}", e),
            }
        }
        Err(e) => println!("Loading Error: {:?}", e),
    }
    println!("done");
}
