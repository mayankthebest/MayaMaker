# MayaMaker

[![Build Status](https://mayank.visualstudio.com/MayaMaker/_apis/build/status/mayankthebest.MayaMaker?branchName=master)](https://mayank.visualstudio.com/MayaMaker/_build/latest?definitionId=3&branchName=master)

A tool for creating scenarios which capture a patient's hospitalization life cycle and generate HL7 2.3 messages. This tool generates realistic (fake) HL7 2.3 messages. You can generate messages for one encounter or all the encounters that a person may have in their lifetime. It uses the output generated by Synthea and introduces a scheduling algorithm between consecutive encounters. Each time you click the buttons on the tool you will get data from one of the 1000 patients data loaded into the system. You can access the tool here - https://mayamaker.azurewebsites.net/

This is very much a work in progress. Currently the tool supports the follwing message types - A01, A02, A03, A04, A06, A08, A11, A13, A15, A16.
