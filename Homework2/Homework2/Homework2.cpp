#include "stdafx.h"
#include <iostream>
#include <stdio.h>
#include <ctime>
#include <fstream>

using namespace std;

int main(int argc, char *argv[])
{
	clock_t start;
	double time, rate;
	int count = 0;

	//Parsing parameters given to be used in program
	char BuffSize[30];
	strcpy(BuffSize, argv[1]);
	long bufferSize = strtol(BuffSize, NULL, 10);

	char fileNameIn[30];
	strcpy(fileNameIn, argv[2]);

	char fileNameOut[30];
	strcpy(fileNameOut, argv[3]);

	ifstream sourceFile(fileNameIn, ios::binary);
	ofstream  newFile(fileNameOut, ios::binary);

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

		char* buffer = new char[bufferSize];

		while (sourceFile.read(buffer, bufferSize))
		{
			newFile.write(buffer, bufferSize);
			count++;
		}
		//Counts the remaining bytes if the amount isn't an equal multiple of the buffer size
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
	printf("Copied %d bytes in %f milliseconds at the rate of %.3f bytes per milliseconds.\n", byteAmount, time, rate);

	return 0;
}