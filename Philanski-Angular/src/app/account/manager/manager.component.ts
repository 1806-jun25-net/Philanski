import { Component, OnInit } from '@angular/core';
//import { Router } from '@angular/router'
import { PhilanskiApiService } from '../../philanski-api.service';
import { TimeSheetApproval } from '../../models/TimeSheetApproval'
import { HttpErrorResponse, HttpHeaderResponse,HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css']
})
export class ManagerComponent implements OnInit {

  constructor(private apiService: PhilanskiApiService) { }

  TSAs : TimeSheetApproval[];

  ngOnInit() {
  }

  myFunction(TSA : any) {
    document.getElementById(TSA.id).classList.toggle("show");
  }


  onGetTSAs(){
    this.apiService.getTSAsForApproval().subscribe(
      (data : HttpResponse<TimeSheetApproval[]>) => {
        this.TSAs = data.body
        console.log(data.status)
        console.log(data.headers)
    },
    (err : HttpErrorResponse) => {
      console.log("error")
    })}

  onPutTSAApprove(TSA : TimeSheetApproval){
    TSA.status = '1';
    this.apiService.putTSA(TSA).subscribe(
      (data : HttpResponse<TimeSheetApproval>) => {
        console.log("updated Timesheet approval")
        console.log("get timesheets again?")
      },
      (err : HttpErrorResponse) =>{
        console.log(err.status)

      }
    )
  }

  onPutTSADeny(TSA : TimeSheetApproval){
    TSA.status = '2';
    this.apiService.putTSA(TSA).subscribe(
      (data : HttpResponse<TimeSheetApproval>) => {
        console.log("updated Timesheet approval")
        console.log("get timesheets again?")
      },
      (err : HttpErrorResponse) =>{
        console.log(err.status)

      }
    )
  }

}
