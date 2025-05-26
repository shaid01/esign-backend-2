-- Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: SQL Network Interfaces, error: 26 - Error Locating Server/Instance Specified)
	
	- Open "Windows Defender Firewall with Advanced Security"
	- "Inbound Rules" -> "New Rule" -> Program -> c:\Program Files (x86)\Microsoft SQL Server\90\Shared\sqlbrowser.exe

	- Services -> "SQL Server (SQLEXPRESS)" -> Restart

-- How to remove event log name (PowerShell Admin):

PS> Remove-EventLog -LogName "MyLog"

-- How to run script (PowerShell Admin)

PS> cd C:\my_path\yada_yada
PS> .\run_import_script.ps1