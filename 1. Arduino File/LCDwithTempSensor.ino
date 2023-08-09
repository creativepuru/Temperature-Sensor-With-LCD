#include <LiquidCrystal.h>

const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

const int lm35pin = A1;

void setup() {
  Serial.begin(9600);
  lcd.begin(16, 2);
  lcd.print("Temp Sensor:");
}

void loop() {

  int temp_adc_val;
int temp_val;

  temp_adc_val = analogRead(lm35pin);	/* Read Temperature */
  temp_val = (temp_adc_val * 5);	/* Convert adc value to equivalent voltage */
  temp_val = (temp_val/10);	/* LM35 gives output of 10mv/°C */

  lcd.setCursor(0,1);
  lcd.print("Temperature = ");
  lcd.print(temp_val);
  Serial.print("#");
  Serial.print(temp_val);
  Serial.print("@");
  lcd.print(" Degree Celsius\n");
  delay(500);
}
