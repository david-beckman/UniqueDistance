# Unique Distance Problem
[Think-Maths Unique Distance Problem](https://think-maths.co.uk/uniquedistance)

Is it always possible to put n counters on an n x n grid, such that no two counters are the same distance apart?

*Puzzle for Submission:* Can you place 6 counters on a 6x6 grid such that the distance between each counter is different?

# Solutions
## 1x1 Board
1 Solutions:
```
X
```

## 2x2 Board
2 Solutions:
```
XX
--

X-
-X
```

## 3x3 Board
5 Solutions:
```
XX-
--X
---

X-X
X--
---

XX-
---
-X-

XX-
---
--X

X--
-XX
---
```

## 4x4 Board
23 Solutions:
```
X--X
X---
-X--
----

XX--
--X-
----
--X-

--XX
X---
----
X---

XX--
----
X--X
----

X-X-
X--X
----
----

X-X-
X---
----
---X

X--X
X-X-
----
----

X---
X-X-
---X
----

X---
X--X
--X-
----

XX--
----
--X-
---X

XX--
----
---X
-X--

X---
X--X
----
---X

X-X-
--X-
----
X---

-X-X
X---
X---
----

-X-X
X---
----
-X--

X--X
-X--
----
-X--

X--X
----
-X--
-X--

X--X
----
-X--
--X-

---X
X-X-
X---
----

--X-
XX--
----
---X

-XX-
X---
----
--X-

--X-
X--X
X---
----

-XX-
----
-X--
--X-
```

## 5x5 Board
35 Solutions:
```
XX--X
-----
-X---
-----
---X-

X--XX
-----
-X---
-----
-X---

---XX
X-X--
-----
-----
X----

X--X-
X----
-X---
-----
----X

X---X
X----
-X---
----X
-----

X----
X---X
-X---
-----
----X

XX---
--X--
-----
-----
--X-X

X----
X---X
-----
-X--X
-----

---XX
X----
-X---
-----
X----

-X--X
---X-
-----
X----
X----

XX---
-----
X--X-
----X
-----

X-X--
X----
----X
---X-
-----

X-X--
X----
-----
---X-
----X

X--X-
X-X--
-----
-----
----X

X---X
----X
-X---
X----
-----

----X
X---X
-X---
-----
X----

-X--X
---X-
-----
-----
XX---

-XX-X
-----
-----
X----
--X--

X---X
-----
XX---
----X
-----

X-X--
---XX
-----
-----
X----

X-X--
---X-
-----
----X
X----

--X-X
-----
X----
-XX--
-----

--X--
---XX
X----
-----
X----

-X--X
-----
X---X
X----
-----

X----
-X---
-X--X
-----
----X

-X---
-X---
X---X
-----
----X

X--X-
-X---
-----
-----
--XX-

----X
XX---
-----
X---X
-----

--XX-
-X---
-----
-----
X--X-

X---X
-X---
-X---
-----
-X---

-X-X-
---X-
-----
-X---
X----

-X---
---XX
-----
-X---
X----

-XX--
X----
-----
--X-X
-----

--XX-
-----
---X-
X----
-X---

--X--
--X-X
-----
X----
-X---
```

## 6x6 Board
2 Solutions:
```
----XX
--X---
------
X-----
------
X--X--

X--X-X
------
------
--X---
-X----
-X----
```

## 7x7 Board
1 Solution:
```
X-X----
--X----
------X
X------
-------
-----X-
------X
```

## 8x8 Board
0 Solutions.

## 9x9 Board
0 Solutions.

# Running
To run the project:
1. [Download & install the latest version of .NET Core 3.1.](https://dotnet.microsoft.com/download/dotnet-core/3.1)
2. Execute one of the following from the project folder (the 6 may be replaced with any board size):
  * Windows: `dotnet run -c Release -r win-x64 -- 6`
  * Mac: `dotnet run -c Release -r osx-x64 -- 6`
  * Linux: `dotnet run -c Release -r linux-x64 -- 6`

Note that the number of combinations is a factorial of `(n * n)! / (n! * (n * n - n)!)` so while a 6x6 board will only take less than a second, a 7x7 will take upwards of a minute, an 8x8 board will take upwards of an hour, a 9x9 will take upwards of a day, a 10x10 will take upwards of a month, and so on.
