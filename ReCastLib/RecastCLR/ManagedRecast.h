
#include "Recast.h"
namespace ManagedRecast {
	public ref class rcContextManaged {
	public:
		rcContextManaged(bool state);
		rcContextManaged();
		!rcContextManaged();
		~rcContextManaged();
		void enableLog(bool state);
		void resetLog();
		void log(const rcLogCategory category, System::String^ format, ...array<Object^>^ args);
		void enableTimer(bool state);
		void resetTimers();
		void startTimer(const rcTimerLabel label);
		void stopTimer(const rcTimerLabel label);
		int getAccumulatedTime(const rcTimerLabel label);
		
	private:
		rcContext* instanceptr;
	};
}