import { Component } from '@angular/core';
import { FeatureFormComponent } from './feature-form.component'
              
@Component({
  template: `
    <view-list
      [title]="'Features' | translate"
      [allowSearchFields]="true"
      [dataSource]="'Feature'"
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
    </view-list>
  `,
})
export class FeatureComponent {
  form: any = FeatureFormComponent;
}
