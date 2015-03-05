
int RED_PIN = 5;
int GRN_PIN = 6;
int RED_ON = 100;
int GRN_ON = 75;

int mode = 0;
void setup() {
  pinMode(RED_PIN, OUTPUT);
  pinMode(GRN_PIN, OUTPUT);
  //Serial.begin(57600);
}

void loop() {
  analogWrite(GRN_PIN, 0);
  analogWrite(RED_PIN, RED_ON);
  delay(1000);
  analogWrite(RED_PIN, 0);  
  analogWrite(GRN_PIN, GRN_ON);
  delay(1000);
  analogWrite(RED_PIN, RED_ON);
  delay(1000);
  
  
  //if (Serial.available() > 0) {
  //  parseOpCode(Serial.read());
  //}
}

void parseOpCode(int opCode) {
}
