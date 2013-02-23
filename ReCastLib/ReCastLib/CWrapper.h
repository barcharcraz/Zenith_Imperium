#include "Recast.h"

#ifndef CWRAPPER_H
#define CWRAPPER_H


extern "C" {
#pragma region rcContextClass
	__declspec(dllexport) rcContext* CreateRcContext(bool state = true);
	__declspec(dllexport) void enableLog(rcContext* classptr, bool state);
	__declspec(dllexport) void resetLog(rcContext* classptr);
	__declspec(dllexport) void log(rcContext* classptr, const rcLogCategory category, const char* format, ...);
	__declspec(dllexport) void enableTimer(rcContext* classptr, bool state);
	__declspec(dllexport) void resetTimers(rcContext* classptr);
	__declspec(dllexport) void startTimer(rcContext* classptr, const rcTimerLabel label);
	__declspec(dllexport) void stopTimer(rcContext* classptr, const rcTimerLabel label);
	__declspec(dllexport) int getAccumulatedTime(rcContext* classptr, const rcTimerLabel label);
#pragma endregion



}


#endif