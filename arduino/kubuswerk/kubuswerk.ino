
// Constant definitions
int RED_PIN    = 5;
int GRN_PIN    = 6;
int RED_ON     = 100;
int GRN_ON     = 75;
int MODE_OFF   = 0;
int MODE_ON    = 1;
int MODE_BLINK = 2;
int MODE_PULSE = 3;

int greenMode  = 0;
int redMode    = 0;
int greenReps  = 0;
int redReps    = 0;

void setup() {
  pinMode(RED_PIN, OUTPUT);
  pinMode(GRN_PIN, OUTPUT);
  Serial.begin(57600);
}

void loop() {
  if (Serial.available() > 0) {
    parseOpCode(Serial.read());
  }
  runOn();
  runOff();
  runBlink();
  runPulse();
}

void runOn() {
}

void runOff() {
  
}

void runBlink() {

}

void runPulse() {
}

int OP_RED   = 0b10000000;
int OP_GRN   = 0b01000000;
int OP_MODE  = 0b00110000;
// EXAMPLE     0b11100011;
int OP_REPS  = 0b00001111;
void parseOpCode(int opCode) {
  int mode = (opCode & OP_MODE) >> 4;
  int reps = (opCode & OP_REPS);
  if (opCode & OP_RED) {
    redMode = mode;
    redReps = reps;
  }
  if (opCode & OP_GRN) {
    greenMode = mode;
    greenReps = reps;
  }
}
