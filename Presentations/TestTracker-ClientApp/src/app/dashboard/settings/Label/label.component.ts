import { Component } from '@angular/core';
import { LabelFormComponent } from './label-form.component';

@Component({
  template: `
    <view-list
      [title]="'Labels' | translate"
      [allowSearchFields]="true"
      [dataSource]="'Labels'"
      [addItem]="form"
      [editItem]="form"
    >
      <column-view
        headerText="Id"
        field="id"
        [allowSearching]="false"
        width="50px"
      >
        <ng-template let-item>
          <span class="text-blue-300 p-1 hover:surface-hover cursor-pointer">
            {{ item.id }}
          </span>
        </ng-template>
      </column-view>

      <column-view headerText="Name" field="name"></column-view>
      <column-view headerText="Description" field="description"></column-view>
      <column-view headerText="Color" field="color"></column-view>
    </view-list>
  `
})
export class LabelComponent {
  form: any = LabelFormComponent;
}
