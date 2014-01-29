Water-Tank-Problem
==================

Suppose you have graph G of watertanks. Each watertank n_i in G has a capcity of c_i. One of these watertanks is designated as the "Start" watertank and one of these watertanks is designated as the "End" watertank. Each tank starts off with some amount of water. 

Suppose further that you have a boat at the "Start" watertank. You may transfer that boat to any of the neighboring watertank, but you must transfer at least 1 unit of water to do so. 

Find the shortest path such that every watertank n_i in G has the same amount of water and the boat ends up in the "End" watertank. You may pass through any watertank as many times as you like. 

Limitations or modifications to this problem are as follows:
	- You may or may not overflow a watertank
	- You may or may not have discrete amounts of water; the amount of water must be expressed as an integer
	- You may or may not require that the amount of water in each watertank must be maximal; you must maximize the amount of water in each watertank
