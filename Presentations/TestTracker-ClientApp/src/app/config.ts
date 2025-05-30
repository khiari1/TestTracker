import { InjectionToken } from "@angular/core";
import { FileService } from "./shared/file.service";

export const BASEURL_CONFIG = new InjectionToken<string>('config') ;

export const FILE_SERVICE = new InjectionToken<FileService>('');
