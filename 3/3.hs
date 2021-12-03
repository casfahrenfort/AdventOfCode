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
        print (lines contents)
        print $ part2 (lines contents)

part1 :: [String] -> Int
part1 s = (gamma s) * (epsilon s)

gamma :: [String] -> Int
gamma s = bin2dec $ map mostCommon (transpose s)

epsilon :: [String] -> Int
epsilon s = bin2dec $ map leastCommon (transpose s)

f :: Eq a => a -> Int -> [[a]] -> [[a]]
f d n y = filter ((== d) . (!! n)) y

g :: ([Char] -> Char) -> [Char] -> Int -> [[Char]] -> [Char]
g c ys n s | length s == 1 = head s
           | otherwise = g c (map c (transpose q)) (n+1) q
    where q = f (ys !! n) n s

part2 :: [String] -> Int
part2 s = (bin2dec $ g mostCommon (map mostCommon (transpose s)) 0 s) * (bin2dec $ g leastCommon (map leastCommon (transpose s)) 0 s)

mostCommon :: [Char] -> Char
-- Check if number of 1s and 0s is the same
mostCommon s | and $ map (==head p) (tail p) = '1'
             | otherwise = head (maximumBy (compare `on` length) (group (sort s)))
    where p = map length $ q
          q = (group . sort) s


leastCommon :: [Char] -> Char
leastCommon s | and $ map (==head p) (tail p) = '0'
              | otherwise = head (minimumBy (compare `on` length) (group (sort s)))
    where p = map length $ q
          q = (group . sort) s

bin2dec :: String -> Int
bin2dec = foldr (\c s -> s * 2 + c) 0 . reverse . map c2i
    where c2i c = if c == '0' then 0 else 1