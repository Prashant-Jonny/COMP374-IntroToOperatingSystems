// Homework2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <stdio.h>
#include <ctime>
#include <fstream>


//http://stackoverflow.com/questions/10195343/copy-a-file-in-a-sane-safe-and-efficient-way
//Should run with a command like:
//$ CopyFile.exe 4096 my_source_file.txt new_file_txt 

//argv[0] = name of executable
//argv[1] = buffer size
//argv[2] = source file
//argv[3] = name of copy file

using namespace std;

int _tmain(int argc, char *argv[])
{
	clock_t start, end;
	double time, rate;
	int byteAmount = 1024; //Need to figure out how to determine amount of bytes transferred.
	long bufferSize = strtol(argv[1], NULL, 10); //Need to figure out how to transfer by specific amount of bytes at a time
	ifstream sourceFile;
	ofstream  newFile;

	char fileName[30];
	strcpy(fileName, argv[3]);
	strcat(fileName, ".txt");

	//Open source and create new file to copy to.
	sourceFile.open( argv[2] ); //Is only taking in first char as input
	newFile.open(fileName, ios::out);

	//If too many arguments are given end execution
	if (argc > 4)
	{
		cerr << "Too many parameters given." << endl;
		cerr << "Parameters should be : Executable name, buffer size, source file name, copied file name." << endl;
		return 1;
	}

	//If everything opens ok then begin timing and copying
	if ( sourceFile.is_open() && newFile.is_open() )
	{
		start = clock();

		//Needs modification to do it by variable buffer sizes
		sourceFile.seekg(0, ios::end);
		//ifstream::pos_type s = sourceFile.tellg();
		ifstream::pos_type s = bufferSize;
		sourceFile.seekg(0);
		char* buffer = new char[s];

			sourceFile.read(buffer, s);
			newFile.write(buffer, s);		

		//Cleanup of memory
		end = clock();
		delete[] buffer;
		sourceFile.close();
		newFile.close();
	}
	//If files don't open up then give error and close program.
	else
	{
		cerr << "Chosen source file " << argv[2] << " can't be open or doesn't exist." << endl;
		return 1;
	}

	//Calculate time spent and print result.
	//byteAmount = counter * bufferSize;
	time = end - start ;
	rate = byteAmount / time;
	cout << "Copied " << byteAmount<< " bytes in " << time << " seconds at the rate of " << rate << " bytes per millisecond." << endl;


	return 0;
}

