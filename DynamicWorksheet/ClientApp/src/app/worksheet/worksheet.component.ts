import { AfterViewInit, Component, OnInit } from '@angular/core';

                  fieldData.setValue(foundRes.fieldValue);
                }
              }

              }
              }
             } 
            // if passed fieldOptionId is in dynamic field than don't do anything 
            let isfound = dynamicFieldOption.find(option => option.fieldOptionId == fields.fieldOptionId)
            return (!(fields.fieldId == fieldId && !isfound));
          } else {
          }
        })
      return;
    }

    const fieldOptionId = FieldOption != null && selectedIndex == undefined ? FieldOption : FieldOption.options[selectedIndex].getAttribute('data-FieldOptionId');
    }