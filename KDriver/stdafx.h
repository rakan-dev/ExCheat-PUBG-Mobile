#pragma once
#include <ntifs.h>
#include <ntddk.h>
#include <ntdddisk.h>
#include <scsi.h>
#include <intrin.h>
#include "structs.h"

typedef struct info_t {
	int pid = 0;
	DWORD_PTR address;
	void* value;
	SIZE_T size;
	PVOID  data;
}info, *p_info;

#define ctl_openprocess    CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0365, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
#define ctl_write    CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0366, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
#define ctl_read    CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0367, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
#define ctl_base    CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0368, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
#define ctl_clear	CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0369, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
#define ctl_peb	CTL_CODE(FILE_DEVICE_UNKNOWN, 0x0370, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)

//io
NTSTATUS unsupported_io(PDEVICE_OBJECT device_obj, PIRP irp);
NTSTATUS create_io(PDEVICE_OBJECT device_obj, PIRP irp);
NTSTATUS close_io(PDEVICE_OBJECT device_obj, PIRP irp);

// memory
void read_mem(int pid, void* addr, void* value, size_t size);
void write_mem(int pid, void* addr, void* value, size_t size);
NTSTATUS MyNtOpenProcess(
	PHANDLE ProcessHandle,
	int ProcessId,
	KPROCESSOR_MODE AccessMode
);


extern "C" {
	NTKERNELAPI NTSTATUS IoCreateDriver(PUNICODE_STRING DriverName, PDRIVER_INITIALIZE InitializationFunction);
	NTKERNELAPI NTSTATUS ZwQuerySystemInformation(SYSTEM_INFORMATION_CLASS SystemInformationClass, PVOID SystemInformation, ULONG SystemInformationLength, PULONG ReturnLength);
	NTKERNELAPI NTSTATUS ObReferenceObjectByName(PUNICODE_STRING ObjectName, ULONG Attributes, PACCESS_STATE PassedAccessState, ACCESS_MASK DesiredAccess, POBJECT_TYPE ObjectType, KPROCESSOR_MODE AccessMode, PVOID ParseContext, PVOID * Object);
	NTKERNELAPI NTSTATUS MmCopyVirtualMemory(PEPROCESS SourceProcess, PVOID SourceAddress, PEPROCESS TargetProcess, PVOID TargetAddress, SIZE_T BufferSize, KPROCESSOR_MODE PreviousMode, PSIZE_T ReturnSize);
	NTKERNELAPI PVOID PsGetProcessSectionBaseAddress(PEPROCESS Process);
	NTKERNELAPI NTSTATUS NTAPI SeCreateAccessState(PACCESS_STATE AccessState,PVOID AuxData,ACCESS_MASK DesiredAccess,PGENERIC_MAPPING Mapping);
	NTKERNELAPI VOID NTAPI SeDeleteAccessState(PACCESS_STATE AccessState);
	NTKERNELAPI PPEB NTAPI PsGetProcessPeb(IN PEPROCESS Process);
}
