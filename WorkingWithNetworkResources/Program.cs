﻿using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using static System.Console;

Write("Enter a valid web address: "); 
string? url = ReadLine(); 
if (string.IsNullOrWhiteSpace(url)) { 
    url = "https://stackoverflow.com/search?q=securestring"; 
}
Uri uri = new(url); 
WriteLine($"URL: {url}"); 
WriteLine($"Scheme: {uri.Scheme}"); 
WriteLine($"Port: {uri.Port}"); 
WriteLine($"Host: {uri.Host}"); 
WriteLine($"Path: {uri.AbsolutePath}"); 
WriteLine($"Query: {uri.Query}");

IPHostEntry entry = Dns.GetHostEntry(uri.Host); 
WriteLine($"{entry.HostName} has the following IP addresses:"); 
foreach (IPAddress address in entry.AddressList) { 
    WriteLine($" {address} ({address.AddressFamily})"); 
}

try { 
    Ping ping = new(); WriteLine("Pinging server. Please wait..."); 
    PingReply reply = ping.Send(uri.Host); 
    WriteLine($"{uri.Host} was pinged and replied: {reply.Status}."); 
    if (reply.Status == IPStatus.Success) { 
        WriteLine("Reply from {0} took {1:N0}ms", arg0: reply.Address, arg1: reply.RoundtripTime); 
    } 
} catch (Exception ex) { 
    WriteLine($"{ex.GetType().ToString()} says {ex.Message}"); 
}
