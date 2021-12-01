import System.IO
import Control.Monad

main = do  
        contents <- readFile "1.txt"
        print (b 0 (map (read :: String -> Int) . words $ contents))

a :: Int -> [Int] -> Int
a c (y:[]) = c
a c (x:y:xs) | x < y = a (c+1) (y:xs)
             | otherwise = a c (y:xs) 

b :: Int -> [Int] -> Int
b c (y:z:q:[]) = c
b c (x:y:z:q:xs) | x+y+z < y+z+q = b (c+1) (y:z:q:xs)
                 | otherwise = b c (y:z:q:xs)