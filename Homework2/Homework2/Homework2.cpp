// Homework2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <stdio.h>
#include <ctime>
#include <fstream>
#include <iomanip> //setprecision()


//http://stackoverflow.com/questions/10195343/copy-a-file-in-a-sane-safe-and-efficient-way
//Should run with a command like:
//$ CopyFile.exe 4096 my_source_file.txt new_file_txt 

//argv[0] = name of executable
//argv[1] = buffer size
//argv[2] = source file
//argv[3] = name of copy file

using namespace std;

int main(int argc, char *argv[])
{
	clock_t start;
	double time, rate;
	//int byteAmount = 1024; //Need to figure out how to determine amount of bytes transferred.
	int count = 0;
	char BuffSize[30];
	strcpy(BuffSize, argv[1]);
	long bufferSize = strtol(BuffSize, NULL, 10); //Need to figure out how to transfer by specific amount of bytes at a time

	char fileNameIn[30];
	strcpy(fileNameIn, argv[2]);
	strcat(fileNameIn, ".txt");

	char fileNameOut[30];
	strcpy(fileNameOut, argv[3]);
	strcat(fileNameOut, ".txt");

	ifstream sourceFile(fileNameIn, ios::binary);
	ofstream  newFile(fileNameOut, ios::binary);

	//Open source and create new file to copy to.
	//sourceFile.open( fileNameIn ); //Is only taking in first char as input
	//newFile.open(fileNameOut, ios::out);

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
		//sourceFile.seekg(0, ios::end);
		//ifstream::pos_type s = sourceFile.tellg();
		//ifstream::pos_type s = 0;
		//sourceFile.seekg(0);
		char* buffer = new char[bufferSize];

		while (sourceFile.read(buffer, bufferSize))
		{
			//sourceFile.read(buffer, bufferSize);
			newFile.write(buffer, bufferSize);
			count++;
		}
		if (sourceFile.gcount())
		{
			newFile.write(buffer, sourceFile.gcount());
		}

		clock_t end = clock();

		time = (double)(end - start);

		//Cleanup of memory	
		delete[] buffer;
		sourceFile.close();
		newFile.close();
	}
	//If files don't open up then give error and close program.
	else
	{
		cerr << "Chosen source file " << fileNameIn << " can't be open or doesn't exist." << endl;
		return 1;
	}

	//Calculate time spent and print result.	
	int byteAmount = (count * bufferSize) + sourceFile.gcount();
	rate = byteAmount / time;
	//cout << setprecision(5) <<  "Copied " << byteAmount << " bytes in " << time << " seconds at the rate of " << rate << " bytes per millisecond." << endl;
	printf("Copied %d bytes in %.10f milliseconds at the rate of %.5f bytes per millisecond\n", byteAmount, time, rate);

	return 0;
}