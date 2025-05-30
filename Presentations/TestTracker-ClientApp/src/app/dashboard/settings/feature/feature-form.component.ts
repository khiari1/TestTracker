import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Feature } from 'src/app/models/feature.model';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-feature-form',
  template: `
    <modal-form-view [dataSource]="'Feature'" [title]="'Feature'" [(data)]="feature">
      <ng-template #content>
        <div class="card" *ngIf="feature">
          <div class="formgrid p-fluid">
            <div class="field">
              <label for="name">{{ 'Name' | translate }}</label>
              <input
                pInputText
                [(ngModel)]="feature.name"
                id="name"
                type="text"
              />
            </div>
            <div class="field">
              <label for="description">{{ 'Description' | translate }}</label>
              <input
                pInputText
                [(ngModel)]="feature.description"
                id="description"
                type="text"
              />
            </div>
          </div>
        </div>
      </ng-template>
    </modal-form-view>
  `,
  providers: [MessageService],
})
export class FeatureFormComponent implements OnInit {

  feature: Feature = new Feature();

  constructor(public config: DynamicDialogConfig) { }

  ngOnInit(): void {}
}
