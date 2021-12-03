import System.IO
import Control.Monad
import Data.List
import Data.Function
import Data.Char (digitToInt)
import Data.Maybe (listToMaybe)
import Data.Sequence (mapWithIndex)
import Numeric    (readInt)

main = do  
        contents <- readFile "3.txt"
        print $ part1 (lines contents)
        print $ part2 (lines contents)

part1 :: [String] -> Int
part1 s = (gamma s) * (epsilon s)

gamma :: [String] -> Int
gamma s = bin2dec $ map (commonBit maximumBy) (transpose s)

epsilon :: [String] -> Int
epsilon s = bin2dec $ map (commonBit minimumBy) (transpose s)

filterNthDigit :: Eq a => a -> Int -> [[a]] -> [[a]]
filterNthDigit digit n list = filter ((== digit) . (!! n)) list

filterReports :: ([Char] -> Char) -> Int -> [[Char]] -> [Char]
filterReports f n [y] = y
filterReports f n s   = filterReports f (n+1) $ filterNthDigit (common !! n) n s
    where common = map f (transpose s)

part2 :: [String] -> Int
part2 input = (bin2dec oxygenBits ) * (bin2dec co2Bits)
    where oxygenBits = filterReports (commonBit maximumBy) 0 input
          co2Bits = filterReports (commonBit minimumBy) 0 input

commonBit :: Foldable t => ((t a1 -> t a1 -> Ordering) -> [[Char]] -> [a2]) -> [Char] -> a2
commonBit f s = head (f (compare `on` length) (group (sort s)))

bin2dec :: String -> Int
bin2dec = foldr (\c s -> s * 2 + c) 0 . reverse . map c2i
    where c2i c = if c == '0' then 0 else 1