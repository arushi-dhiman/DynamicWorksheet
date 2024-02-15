import { Component } from '@angular/core';
import { SharedService } from 'src/Services/SharedService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private sharedService: SharedService) { }

  sendTemplateId(id: number): void {
    this.sharedService.changeTemplateId(id);
  }
}
