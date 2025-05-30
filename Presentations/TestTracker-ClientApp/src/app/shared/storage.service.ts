import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class StorageService { /**
     *
     */
    constructor() { }

    setItem<T>(key: string, object: T) {
        localStorage.setItem(key, JSON.stringify(object));
    }

    getItem<T>(key: string): T | null | any {
        var item = localStorage.getItem(key);
        if (item !== null) {
            return JSON.parse(item);
        } else {
            return null;
        }
    }
    removeItem(key: string) {
        localStorage.removeItem(key);
    }

    clear() {
        localStorage.clear();
    }
}
