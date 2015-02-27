
int RED_PIN = 5;
int GRN_PIN = 6;

int mode = 0;
void setup() {
  pinMode(RED_PIN, OUTPUT);
  pinMode(GRN_PIN, OUTPUT);
  Serial.begin(57600);
}

void loop() {
  if (Serial.available() > 0) {
    parseOpCode(Serial.read());
  }
}

void parseOpCode(int opCode) {
}
