import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CarouselModule, CarouselConfig } from 'ngx-bootstrap/carousel';

@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css'] ,
  providers: [
    { provide: CarouselConfig, useValue: { interval: 3000, noPause: true, showIndicators: true } }
  ]
})
export class HotelDetailsComponent implements OnInit {
  selectedId: any;
  selectedItem: any;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.selectedId = +params.get('id');
      this.getItem(this.selectedId);
    }
    );
  }
  getItem(selectedId: any): any {
    this.http.get("/hotels/" + selectedId).subscribe((r: any) => {
      this.selectedItem = r.hotel;
      console.log(this.selectedItem)

    })
  }

}
