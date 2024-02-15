import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  private templateIdSource = new BehaviorSubject<number>(0);
  currentTemplateId = this.templateIdSource.asObservable();

  changeTemplateId(id: number) {
    this.templateIdSource.next(id);
  }
}
