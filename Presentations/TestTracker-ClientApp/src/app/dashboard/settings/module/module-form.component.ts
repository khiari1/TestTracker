import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-module-form',
  template: `
    <modal-form-view [title]="'Module'" dataSource="Modules" [(data)]="module">
      <ng-template #content>
        <div class="formgrid p-fluid" *ngIf="module">
          <div class="field">
            <label for="name">{{ 'Name' | translate }}</label>
            <input [(ngModel)]="module.name" id="name" type="text" pInputText />
          </div>
        </div>
      </ng-template>
    </modal-form-view>
  `,
})
export class ModuleFormComponent {
  module: any = {};
}
