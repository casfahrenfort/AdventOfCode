import System.IO
import Control.Monad
import Data.List
import Data.List.Split
import Data.Function
import Data.Char (digitToInt)
import Data.Maybe (listToMaybe)
import Data.Sequence (mapWithIndex)
import Numeric    (readInt)

main = do  
        contents <- readFile "4.txt"
        let l = lines contents
        let numbers = map (read :: String -> Int) (splitOn "," (head l))
        let boards = readBoards (drop 2 (lines contents))
        let first = firstWonBoard numbers boards
        print $ boardScore first
        let last = lastWonBoard numbers boards
        print $ boardScore last

data Board = Board [[(Int,Bool)]] | NoBoard 
    deriving (Eq, Show)

lastWonBoard :: [Int] -> [Board] -> (Board,Int)
lastWonBoard (x:[]) boards = (NoBoard,x)
lastWonBoard (x:xs) boards | allWon = (markNumber x (head $ filter (not . hasWon) boards),x)
                           | otherwise = lastWonBoard xs markedBoards
    where allWon = all hasWon markedBoards
          markedBoards = map (markNumber x) boards

firstWonBoard :: [Int] -> [Board] -> (Board,Int)
firstWonBoard (x:[]) boards = (NoBoard,x)
firstWonBoard (x:xs) boards = if wonBoard == NoBoard then firstWonBoard xs markedBoards else (wonBoard,x)
    where wonBoard = winningBoard markedBoards
          markedBoards = map (markNumber x) boards

boardScore :: (Board,Int) -> Int
boardScore ((Board rows),lastNumber) = unmarkedSum * lastNumber
    where unmarkedSum = sum $ map (foldl score 0) rows
          score x (i,True) = x
          score x (i,False) = x+i

markNumber :: Int -> Board -> Board
markNumber number (Board rows) = Board (map checkNumbers rows)
    where checkNumbers ((i,b):[]) = if i == number then [(i,True)] else [(i,b)]
          checkNumbers ((i,b):xs) = if i == number then (i,True):(checkNumbers xs) else (i,b):(checkNumbers xs)


readBoards :: [String] -> [Board]
readBoards input = makeBoards numbers
    where makeBoards = map Board . map (map (map makeTuple)) . map (take 5) . chunksOf 6
          makeTuple i = (i,False)
          split = map (splitOn " ") input
          filtered = map (filter (/= "")) split
          numbers = map (map (read :: String -> Int)) filtered

winningBoard :: [Board] -> Board
winningBoard boards = if length wonBoards > 0 then head wonBoards else NoBoard
    where wonBoards = filter hasWon boards

hasWon :: Board -> Bool
hasWon (Board rows) = any allMarked rows || any allMarked (transpose rows)
    where allMarked = all (\(i,b) -> b)
