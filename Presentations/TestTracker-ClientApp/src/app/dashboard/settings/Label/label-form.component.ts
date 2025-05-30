import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Label } from 'src/app/models/label.model';

@Component({
  selector: 'app-label-form',
  template: `
    <modal-form-view [dataSource]="'Labels'" [title]="'Label'" [(data)]="label">
      <ng-template #content>
        <div class="card" *ngIf="label">
          <div class="formgrid p-fluid">
            <div class="field">
              <label for="name">{{ 'Name' | translate }}</label>
              <input pInputText [(ngModel)]="label.name" id="name" type="text" />
            </div>
            <div class="field">
              <label for="description">{{ 'Description' | translate }}</label>
              <input pInputText [(ngModel)]="label.description" id="description" type="text" />
            </div>
            <div class="field">
              <label for="color">{{ 'Color' | translate }}</label>
              <p-colorPicker [(ngModel)]="label.color" id="color"></p-colorPicker>
            </div>
          </div>
        </div>
      </ng-template>
    </modal-form-view>
  `
})
export class LabelFormComponent implements OnInit {
  label: Label = new Label();

  constructor() { }

  ngOnInit(): void {
  }
}
