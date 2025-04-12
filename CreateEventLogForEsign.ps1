$sourceName = "EsignWebAPI"
$logName = "ComdaEsign"

if (! [System.Diagnostics.EventLog]::SourceExists($sourceName)) {
    New-EventLog -LogName $logName -Source $sourceName

    Write-EventLog -LogName $logName -Source $sourceName -EventId 100 -EntryType Information -Message "EventSource Installed"
    exit
}

Write-EventLog -LogName $logName -Source $sourceName -EventId 100 -EntryType Information -Message "EventSource already existed"