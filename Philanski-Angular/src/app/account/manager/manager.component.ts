import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
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

  onGetTSAs(){
    this.apiService.getTSAsForApproval().subscribe(
      (data : HttpResponse<TimeSheetApproval[]>) => {
        this.TSAs = data.body
        debugger; 
        console.log(data.status)
        console.log(data.headers)
    },
    (err : HttpErrorResponse) => {
      console.log("error")
    }
    )

  }

}
