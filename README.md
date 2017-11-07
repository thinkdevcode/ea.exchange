# Introduction

I was interested in the process behind building a bitcoin exchange that was actually fast (this was in the MtGox days) so I wrote a very simple single asset limit trading app in .NET for fun, and learned a bunch of interesting things on the way.

# Market Background

There are only two order books:

- Buy
- Sell

There are many [different kinds of order types](http://en.wikipedia.org/wiki/Order_(exchange)) in a real market but this app only does the basic *limit* orders. Adding in *market* orders would be pretty trivial as all you would have to do is use either **Min()** or **Max()** on the respective book. Another thing to look at would be to add the order constraints [Fill or Kill](http://en.wikipedia.org/wiki/Fill_or_kill) and [All or None](http://en.wikipedia.org/wiki/All_or_none). Right now, the orders are partially filled.

# The Order Books

For the order books I used a [SortedSet](http://msdn.microsoft.com/en-us/library/dd412070(v=vs.110).aspx) which in .NET uses a [Red-Black Tree](http://en.wikipedia.org/wiki/Red%E2%80%93black_tree) data structure. A SortedSet/Red-Black Tree is a self-balancing binary search tree, which basically means that as orders are added to the books they are automatically sorted for us. The orders aren't primitive types so for the **Order** class we needed to implement an [IComparer](http://msdn.microsoft.com/en-us/library/8ehhxeaf(v=vs.110).aspx) that we aptly called **ByLimit** which is passed into the books on initialization.

# Time

This was actually a huge surprise to me while building this application because I'm not used to developing under such tiny time constraints. My first thought was of course to use a [DateTime](http://msdn.microsoft.com/en-us/library/system.datetime.aspx), but found out that it is not precise at all (when dealing with time on a nanosecond level). In fact, DateTime's only go down to about a 16 millisecond "precision", which for trading is abysmal. This led me to investigate DateTime's, and the [difference between precision and accuracy](http://blogs.msdn.com/b/ericlippert/archive/2010/04/08/precision-and-accuracy-of-datetime.aspx).

I ended up having to use [StopWatch](http://msdn.microsoft.com/en-us/library/system.diagnostics.stopwatch.aspx) because the trades were executing way faster than a DateTime could handle. This poses an interesting question as to how to store the books in a database. Right now, when you initialize a new **OrderBook** a stopwatch is started and that is whats used to determine how many "ticks" have passed and that is what's used in the **Order** to determine the order in which trades occurred.

# Benchmarks

Dev laptop: i7-3632QM @ 2.2GHz & 16GB: 
Run over 10 seconds. 
Orders submitted: *1,505,542* or **150,554** orders per second 
Trades completed: *1,010,481* or **101,048** trades per second

# In Summary

There's a lot that goes into building a high performance exchange but it seems to be doable in .NET as long as the trade volume isn't crazy (like NASDAQ).

# TL;DR

Markets have many different kinds of order types, limit being the simplest
SortedSet is a .NET implementation of a Red-Black Tree
Use it for order books
DateTime has terrible precision
Use stopwatch instead
