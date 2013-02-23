#include "ManagedRecast.h"
#include <msclr/marshal.h>
using namespace msclr::interop;
using namespace ManagedRecast;
rcContextManaged::rcContextManaged(bool state) {
	instanceptr = new rcContext(state);
}
rcContextManaged::rcContextManaged() {
	instanceptr = new rcContext();
}
rcContextManaged::~rcContextManaged() {
	this->!rcContextManaged();
}
rcContextManaged::!rcContextManaged() {
	delete instanceptr;
}
void rcContextManaged::enableLog(bool state) {
	instanceptr->enableLog(state);
}
void rcContextManaged::resetLog() {
	instanceptr->resetLog();
}
void rcContextManaged::log(const rcLogCategory category, System::String^ format, ...array<Object^>^ args) {
	marshal_context^ ctx = gcnew marshal_context();
	instanceptr->log(category, ctx->marshal_as<const char *, System::String^>(format), args);
}
void rcContextManaged::enableTimer(bool state) {
	instanceptr->enableTimer(state);
}
void rcContextManaged::resetTimers() {
	instanceptr->resetTimers();
}
void rcContextManaged::startTimer(const rcTimerLabel label) {
	instanceptr->resetTimers();
}
void rcContextManaged::stopTimer(const rcTimerLabel label) {
	instanceptr->startTimer(label);
}
int rcContextManaged::getAccumulatedTime(const rcTimerLabel label) {
	return instanceptr->getAccumulatedTime(label);
}