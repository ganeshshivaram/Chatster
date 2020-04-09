import { Injectable } from '@angular/core';
import * as alertifyjs from 'alertifyjs';

@Injectable()
export class AlertifyService {
  constructor() {}

  confirm(messsage: string, okCallback: () => any) {
    alertifyjs.confirm(messsage, (eve: any) => {
      if (eve) {
        okCallback();
      } else {
      }
    });
  }

  success(message: string) {
    alertifyjs.success(message);
  }

  error(message: string) {
    alertifyjs.error(message);
  }

  warning(message: string) {
    alertifyjs.warning(message);
  }

  message(message: string) {
    alertifyjs.message(message);
  }
}
