import { Component, OnInit } from '@angular/core';
import { faMapMarkedAlt, faEnvelope, faPhoneSquare } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  //FontAwesomeIcons
  faMapMarkedAlt = faMapMarkedAlt;
  faEnvelope = faEnvelope;
  faPhoneSquare = faPhoneSquare;
  constructor() { }

  ngOnInit() {
  }

}
