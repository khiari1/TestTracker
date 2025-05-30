import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MonitoringService, State } from 'src/app/services/monitoring.service';
interface Type {
  name: string;
  code: number;
}
@Component({
  selector: 'monitoring-widget',
  template: ` <div class="surface-0 px-2 py-5 md:px-6 lg:px-8">
    <div class="grid">
      <div class="col-12 md:col-6 lg:col-3">
        <div class="shadow-1 border-1 bg-gray-100 border-300 px-4 py-5 border-round-sm">
          <div class="flex">
            <div>
              <img src="assets/svg/warning-24.png" title="Skipped" />
            </div>
            <div class="ml-3 mb-2">
              <span class="block text-2xl   font-bold "
                > Skipped Test</span
              >
            </div>
          </div>
          <span class="text-green-700 font-normal">24 new </span>
          <span class="">More detail</span>
        </div>
      </div>
      <div class="col-12 md:col-6 lg:col-3">
        <div class="shadow-1 border-1 bg-gray-100 border-300 px-4 py-5 border-round-sm">
          <div class="flex">
            <div>
              <img src="assets/svg/success.svg" title="Success" />
            </div>
            <div class="ml-3 mb-2">
              <span class="block text-2xl   font-bold "
                > Succeded Test</span
              >
            </div>
          </div>
          <span class="text-green-500 font-normal">24 new </span>
          <span class="text-500">More detail</span>
        </div>
      </div>
      <div class="col-12 md:col-6 lg:col-3">
        <div class=" border-1 shadow-1 bg-gray-100 border-300 px-4 py-5 border-round-sm">
          <div class="flex">
            <div>
              <img src="assets/svg/error.svg" title="Error" />
            </div>
            <div class="ml-3 mb-2">
              <span class="block text-2xl   font-bold "
                > On Error</span
              >
            </div>
          </div>
          <span class="text-green-500 font-normal">24 new </span>
          <span class="text-500">More detail</span>
        </div>
      </div>
      <div class="col-12 md:col-6 lg:col-3">
        <div class="bg-gray-100 shadow-1 border-round-sm border-1 border-300 px-4 py-5">
          <div class="flex"> 
            <div>
              <img src="assets/svg/no-entry-24.png" title="Failed" />
            </div>
            <div class="ml-3 mb-2">
              <span class="block text-2xl   font-bold "
                >Failed Test</span
              >
            </div>
          </div>
          <span class="text-green-500 font-normal">24 new </span>
          <span class="text-500">More detail</span>
        </div>
      </div>
    </div>
  </div>`,
  providers: [DatePipe],
})
export class MonitoringWidgetComponent implements OnInit {
  ngOnInit(): void {
  }
  
  
}
