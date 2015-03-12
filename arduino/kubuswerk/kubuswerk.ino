
// Constant definitions
int RED_PIN    = 5;
int GRN_PIN    = 6;
int RED_ON     = 10;
int GRN_ON     = 10;
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
  while (Serial.available() > 0) {
    parseOpCode(Serial.read());
  }
  int i;
  runOn();
  runOff();
  for (i=0;i<100;i++) {
    runBlink();
    runPulse();
    delay(10);
  }
}

void runOn() {
  if (greenMode == MODE_ON) {
    analogWrite(GRN_PIN, GRN_ON);
  }
  if (redMode == MODE_ON) {
    analogWrite(RED_PIN, RED_ON);
  }
}

void runOff() {
  if (greenMode == MODE_OFF) {
    analogWrite(GRN_PIN, 0);
  }
  if (redMode == MODE_OFF) {
    analogWrite(RED_PIN, 0);
  }
}

int redBlinkState = 0;
int grnBlinkState = 0;

void runBlink() {
  if (greenMode == MODE_BLINK) {
    if (grnBlinkState++ < 50) {
      analogWrite(GRN_PIN, GRN_ON);
    } else {
      analogWrite(GRN_PIN, 0);
    }
    grnBlinkState %= 100;
  }
  if (redMode == MODE_BLINK) {
    if (redBlinkState++ < 50) {
      analogWrite(RED_PIN, GRN_ON);
    } else {
      analogWrite(RED_PIN, 0);
    }
    redBlinkState %= 100;
  }
}

int redPulseState = 0;
int grnPulseState = 0;
void runPulse() {
  if (redPulseState < 50) {
    redPulseState++;
  } else {
    redPulseState--;
  }
  if (grnPulseState < 50) {
    grnPulseState++;
  } else {
    grnPulseState--;
  }
  int redPower = map(redPulseState, 0, 100, 0, RED_ON);
  int grnPower = map(grnPulseState, 0, 100, 0, GRN_ON);
  analogWrite(RED_PIN, redPower);
  analogWrite(GRN_PIN, grnPower);
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
