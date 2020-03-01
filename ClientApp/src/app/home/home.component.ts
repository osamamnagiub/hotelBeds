import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import * as CryptoJS from 'crypto-js';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  inputValue = "";
  APIKEY = "956sxnzbe36anayk6bafmywq";
  SECRET = "9Tuvb5X9cR";
  ENDPOINT = "https://api.test.hotelbeds.com";
  originalResult: any = [];
  searchedResult: any = [];
  total = 0;
  /**
   *
   */
  constructor(private http: HttpClient) {
    this.http.get("/hotels").subscribe((r: any) => {
      this.total = r.total;
      this.originalResult = r.hotels;
      this.searchedResult = this.originalResult;
    })
  }


  public onSearchInputChange(term: string): void {
    this.searchedResult = this.search(this.originalResult, term);
    if(this.searchedResult == null || this.searchedResult.length === 0){
      this.searchedResult = this.originalResult;
    }

  }

  private search(myArray, term){
    for (var i=0; i < myArray.length; i++) {
      if (myArray[i].name.content.indexOf(term) > -1) {
          return myArray[i];
      }
  }
  }
}

