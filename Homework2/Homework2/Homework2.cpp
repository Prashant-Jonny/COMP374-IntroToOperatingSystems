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
	int byteAmount = 1024;
	long bufferSize;

	bufferSize = strtol(argv[1], NULL, 10);
	ifstream sourceFile;
	ofstream  newFile;
	sourceFile.open( argv[2] );
	newFile.open(argv[3], ios::out);

	if (argc > 4)
	{
		cerr << "Too many parameters given." << endl;
		cerr << "Parameters should be : Executable name, buffer size, source file name, copied file name." << endl;
		return 1;
	}

	if ( sourceFile.is_open() && newFile.is_open() )
	{
		start = clock();

		sourceFile.seekg(0, ios::end);
		ifstream::pos_type s = sourceFile.tellg();
		sourceFile.seekg(0);
		char* buffer = new char[s];

		sourceFile.read(buffer, s);
		newFile.write(buffer, s);
		
		end = clock();
		delete[] buffer;
		sourceFile.close();
		newFile.close();
	}
	else
	{
		cerr << "Chosen source file " << argv[2] << " can't be open or doesn't exist." << endl;
		return 1;
	}


	time = ( end - start ) / CLOCKS_PER_SEC;
	rate = byteAmount / time;
	cout << "Copied " << byteAmount<< " bytes in " << time << " seconds at the rate of " << rate << " bytes per second." << endl;


	return 0;
}

